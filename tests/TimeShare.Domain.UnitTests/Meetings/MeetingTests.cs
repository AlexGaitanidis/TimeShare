using FluentAssertions;
using TimeShare.Domain.Aggregates.GuestAggregate;
using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate.Events;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Errors;

namespace TimeShare.Domain.UnitTests.Meetings;

public class MeetingTests
{
    [Fact]
    public void Create_ShouldRaiseDomainEvent_WhenMeetingIsCreated()
    {
        // Act
        var meeting = Meeting.Create(
            "name",
            "description",
            DateTime.UtcNow + TimeSpan.FromHours(1),
            DateTime.UtcNow + TimeSpan.FromHours(5),
            null,
            HostId.CreateUnique(),
            null);

        // Assert
        meeting.GetDomainEvents().LastOrDefault().Should().BeOfType<MeetingCreated>();
    }

    [Fact]
    public void SendInvitation_ShouldReturnError_WhenMeetingIsCancelled()
    {
        // Arrange
        var user1 = User.Create("firstName1", "lastName1", "email1", "password1");
        var user2 = User.Create("firstName2", "lastName2", "email2", "password2");

        var host = Host.Create(user1);

        var guest = Guest.Create(user2);

        var meeting = Meeting.Create(
            "name",
            "description",
            DateTime.UtcNow + TimeSpan.FromHours(1),
            DateTime.UtcNow + TimeSpan.FromHours(5),
            null,
            host.Id,
            null);

        meeting.Cancel();

        // Act
        var result = meeting.SendInvitation(host, guest);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(DomainErrors.Meeting.InvitationForCancelledMeeting);
    }

    [Fact]
    public void SendInvitation_ShouldRaiseDomainEvent_WhenInvitationIsSent()
    {
        // Arrange
        var user1 = User.Create("firstName1", "lastName1", "email1", "password1");
        var user2 = User.Create("firstName2", "lastName2", "email2", "password2");

        var host = Host.Create(user1);

        var guest = Guest.Create(user2);

        var meeting = Meeting.Create(
            "name",
            "description",
            DateTime.UtcNow + TimeSpan.FromHours(1),
            DateTime.UtcNow + TimeSpan.FromHours(5),
            null,
            host.Id,
            null);

        // Act
        var result = meeting.SendInvitation(host, guest);

        // Assert
        meeting.GetDomainEvents().LastOrDefault().Should().BeOfType<InvitationSent>();
    }
}