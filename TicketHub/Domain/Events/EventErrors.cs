using Domain.Abstracts;

namespace Domain.Events
{
    public static class EventErrors
    {
        public static Error NotFound = new Error(
            "Event.NotFound", 
            "Event not found");

        public static Error NotEnoughSpots = new Error(
            "Event.NotEnoughSpots", 
            "Not enough spots available");
    }
}
