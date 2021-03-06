﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NgSwaggerSdkGen.Models.Swagger {
    public class Schema {
        [JsonProperty("$ref")]
        public string @ref;

        public string format;

        public string title;

        public string description;

        public object @default;

        public int? multipleOf;

        public int? maximum;

        public bool? exclusiveMaximum;

        public int? minimum;

        public bool? exclusiveMinimum;

        public int? maxLength;

        public int? minLength;

        public string pattern;

        public int? maxItems;

        public int? minItems;

        public bool? uniqueItems;

        public int? maxProperties;

        public int? minProperties;

        public IList<string> required;

        public IList<object> @enum;

        public string type;

        [JsonProperty("items")]
        public Schema items;

        public IList<Schema> allOf;

        public IDictionary<string, Schema> properties;

        public Schema additionalProperties;

        public string discriminator;

        public bool? readOnly;

        public Xml xml;

        public ExternalDocs externalDocs;

        public object example;

        public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();

        public string GetTypeString() {
            string result = "";
            switch (type) {
                case "object":
                    result = @ref;
                    break;
                case "array":
                    return items.GetTypeString() + "[]";
                case "string":
                    if (@enum != null && @enum.Count > 0) {
                        return $"({string.Join(" | ", @enum.Select(x => x is string ? "'" + Regex.Escape(x.ToString()) + "'" : x))})";
                    }
                    return "string";
                default:
                    result = type ?? @ref;
                    break;
            }

            if (result?.StartsWith("#") ?? false) {
                return PreProcessing.FixTypeName(result.Split('/').Last());
            }

            return result != null ? PreProcessing.FixTypeName(result) : null;
        }
    }
}
