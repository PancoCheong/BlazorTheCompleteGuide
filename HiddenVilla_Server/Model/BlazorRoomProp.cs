// Model\BlazorRoomProp.cs
namespace HiddenVilla_Server.Model
{   //Hotel Room Properties, eg. Occupancy, Square Feet, No. of Toilet etc
    public class BlazorRoomProp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
