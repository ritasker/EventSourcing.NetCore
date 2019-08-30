using System;
using Core.Aggregates;
using MeetingsManagement.Meetings.Events;
using MeetingsManagement.Meetings.ValueObjects;

namespace MeetingsManagement.Meetings
{
    internal class Meeting: Aggregate
    {
        public string Name { get; private set; }

        public DateTime Created { get; private set; }

        public Range Occurs { get; private set; }

        public Meeting()
        {
        }

        public static Meeting Create(Guid id, string name)
        {
            return new Meeting(id, name, DateTime.UtcNow);
        }

        public Meeting(Guid id, string name, DateTime created)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot be empty.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"{nameof(name)} cannot be empty.");

            var @event = MeetingCreated.Create(id, name, created);

            Enqueue(@event);
            Apply(@event);
        }

        public void Apply(MeetingCreated @event)
        {
            Id = @event.MeetingId;
            Name = @event.Name;
            Created = @event.Created;
        }

        internal void Schedule(Range occurs)
        {
            var @event = MeetingScheduled.Create(Id, occurs);

            Enqueue(@event);
            Apply(@event);
        }

        private void Apply(MeetingScheduled @event)
        {
            Occurs = @event.Occurs;
        }
    }
}
