using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Viewer.PoC.Core.Exceptions;
using Viewer.PoC.Core.Helpers;
using Viewer.PoC.Model.Dtos;
using Viewer.PoC.Model.Enums;

namespace Viewer.PoC.Core.Reading
{
    public class JsonFileReader : IFileReader
    {
        public IReadOnlyCollection<string> SupportedExtensions => [".json"];
        private static readonly Dictionary<ShapeTypeEnum, Type> _detectedDtos = DetectShapeDtos.GetDtoTypes();

        public async Task<IReadOnlyList<IShapeDto>> ReadDtosAsync(string path, CancellationToken cancellationToken = default)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var text = File.ReadAllText(path);
                    var array = JArray.Parse(text);
                    var dtos = new List<IShapeDto>();

                    foreach (var jToken in array)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        var typeRaw = (jToken["type"] ?? jToken["Type"])?.Value<string>()?.ToLowerInvariant();
                        if (!Enum.TryParse<ShapeTypeEnum>(typeRaw, true, out var shapeType))
                        {
                            throw new ShapeParseException($"Unknown type in JSON: {typeRaw}");
                        }
                        if (!_detectedDtos.TryGetValue(shapeType, out var dtoType))
                        {
                            throw new ShapeParseException($"Unsupported shape type {shapeType}");
                        }

                        var dto = (IShapeDto)jToken.ToObject(dtoType);
                        dtos.Add(dto);
                    }

                    return (IReadOnlyList<IShapeDto>)dtos;
                }, cancellationToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new ShapeParseException($"Failed to read or parse JSON file '{path}'", ex);
            }
        }
    }
}