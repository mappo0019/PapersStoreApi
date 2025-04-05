using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PapersApi.Models;

public class ProjectPapers{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id {get; set;}
    public required string project {get; set;}
    public required string[] raw {get;set;}

}