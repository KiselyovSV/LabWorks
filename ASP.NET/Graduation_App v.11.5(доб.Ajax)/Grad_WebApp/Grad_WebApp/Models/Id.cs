
namespace Grad_WebApp.Models
{
    public class Id
    {
        internal int? Client_Id { get; set; }

        internal Client_Subscription? ClSub { get; set; }

        internal Timetable_Client? TtblClient { get; set; }

        internal DateTime LastOverTime { get; set; } = DateTime.MinValue;

        internal IList<string>? User_Roles { get; set; }

        internal string? UserId { get; set; }

    }
}
