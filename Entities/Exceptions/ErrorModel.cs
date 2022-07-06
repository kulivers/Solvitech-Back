using System.Text.Json;

namespace Entities
{
    public class ErrorModel
    {
        public ErrorModel()
        {
        }
        public ErrorModel(string code, string description)
        {
            Code = code;
            Description = description;
        }
        public ErrorModel(int code, string description)
        {
            Code = code;
            Description = description;
        }
        public dynamic Code { get; set; }
        public string Description { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}