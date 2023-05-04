using ErrorOr;
using FluentAssertions;
using Moq;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Application.Meetings.Commands.CreateMeeting;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Errors;

namespace TimeShare.Application.UnitTests.Meetings.Commands;

public class CreateMeetingCommandHandlerTests
{
    private readonly Mock<IHostRepository> _hostRepositoryMock;
    private readonly Mock<IMeetingRepository> _meetingRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CreateMeetingCommandHandlerTests()
    {
        _hostRepositoryMock = new();
        _meetingRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenHostDoesNotExist()
    {
        // Arrange
        var command = new CreateMeetingCommand(
            "name",
            "description",
            DateTime.UtcNow + TimeSpan.FromHours(1),
            DateTime.UtcNow + TimeSpan.FromHours(5),
            null,
            HostId.CreateUnique(),
            null);

        _hostRepositoryMock.Setup(x => x.HostExists(command.HostId, default))
            .ReturnsAsync(false);

        var handler = new CreateMeetingCommandHandler(
            _hostRepositoryMock.Object,
            _meetingRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        ErrorOr<Meeting> result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(DomainErrors.Host.NotFound(command.HostId));
    }

    [Fact]
    public async Task Handle_ShouldReturnMeeting_WhenHostExists()
    {
        // Arrange
        var command = new CreateMeetingCommand(
            "name",
            "description",
            DateTime.UtcNow + TimeSpan.FromHours(1),
            DateTime.UtcNow + TimeSpan.FromHours(5),
            null,
            HostId.CreateUnique(),
            null);

        _hostRepositoryMock.Setup(
                x => x.HostExists(command.HostId, default))
            .ReturnsAsync(true);

        var handler = new CreateMeetingCommandHandler(
            _hostRepositoryMock.Object,
            _meetingRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        ErrorOr<Meeting> result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<Meeting>();
    }

    [Fact]
    public async Task Handle_ShouldCallAddOnRepository_WhenHostExists()
    {
        // Arrange
        var command = new CreateMeetingCommand(
            "name",
            "description",
            DateTime.UtcNow + TimeSpan.FromHours(1),
            DateTime.UtcNow + TimeSpan.FromHours(5),
            null,
            HostId.CreateUnique(),
            null);

        _hostRepositoryMock.Setup(
                x => x.HostExists(command.HostId, default))
            .ReturnsAsync(true);

        var handler = new CreateMeetingCommandHandler(
            _hostRepositoryMock.Object,
            _meetingRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        ErrorOr<Meeting> result = await handler.Handle(command, default);

        // Assert
        _meetingRepositoryMock.Verify(
            x => x.Add(It.Is<Meeting>(m => m.Id == result.Value.Id)),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldNotCallUnitOfWork_WhenHostDoesNotExist()
    {
        // Arrange
        var command = new CreateMeetingCommand(
            "name",
            "description",
            DateTime.UtcNow + TimeSpan.FromHours(1),
            DateTime.UtcNow + TimeSpan.FromHours(5),
            null,
            HostId.CreateUnique(),
            null);

        _hostRepositoryMock.Setup(
                x => x.HostExists(command.HostId, default))
            .ReturnsAsync(false);

        var handler = new CreateMeetingCommandHandler(
            _hostRepositoryMock.Object,
            _meetingRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}