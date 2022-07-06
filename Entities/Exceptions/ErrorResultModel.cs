using System.Collections.Generic;
using System.Text.Json;

namespace Entities
{
    public class ErrorResultModel
    {
        public ErrorResultModel(IEnumerable<ErrorModel> errors)
        {
            Errors = errors;
        }

        public bool Succeeded => false;
        public IEnumerable<ErrorModel> Errors { get; set; }
        
        public override string ToString() => JsonSerializer.Serialize(this);

    }
}