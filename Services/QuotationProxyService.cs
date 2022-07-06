using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Entities;
using Solvintech.Services.Contracts;

namespace Solvintech.Services
{
    public static class XmlExtensions
    {
        public static T XmlDeserializeFromString<T>(this string objectData)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        public static object XmlDeserializeFromString(this string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            //var bytes = Encoding.GetEncoding("windows-1251").GetBytes(objectData);
            var s = objectData;
            using (TextReader reader = new StringReader(s))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }
    }

    public sealed class QuotationProxyService : IQuotationProxyService
    {
        public async Task<IEnumerable<ValCursValute>> GetJsonQuotatiation(StringDate stringDate)
        {
            var xml = await CallQuotatiationsApi(stringDate);
            return xml.OuterXml.XmlDeserializeFromString<ValCurs>().Valute;
        }

        private async Task<XmlDocument> CallQuotatiationsApi(StringDate stringDate)
        {
            var (day, month, year) = stringDate;
            var url = $"http://www.cbr.ru/scripts/XML_daily.asp?date_req={day}/{month}/{year}";
            var client = new WebClient();
            var stream = client.OpenRead(url);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException(),
                Encoding.GetEncoding("windows-1251"));
            var content = await reader.ReadToEndAsync();
            var xml = new XmlDocument();
            xml.LoadXml(content);
            return xml;
        }
    }
}