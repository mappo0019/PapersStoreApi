
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PapersApi.Models;

public class Link{
    public required string source {get; set;}
    public required string target {get;set;}
    public required int distance {get;set;}

}