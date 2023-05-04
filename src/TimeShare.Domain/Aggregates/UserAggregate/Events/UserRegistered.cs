﻿using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;
using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.UserAggregate.Events;

public record UserRegistered(Guid Id, UserId UserId) : IDomainEvent;