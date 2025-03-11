using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace PapersApi.Models;


public enum Estado{
    Buscando_Financiacion, 
    En_Progreso, 
    Finalizado,
    Parado
}


public class Project
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id {get; set;}
    public required string name {get; set;}
    public required string main_researcher{get; set;}
    public  Estado? estado {get; set;}
    public string? descripcion {get; set;}
    public string[]? participantes {get;set;}
    public float? presupuesto {get;set;}



}