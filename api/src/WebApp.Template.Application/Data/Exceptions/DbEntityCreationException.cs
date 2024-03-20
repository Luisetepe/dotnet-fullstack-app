namespace WebApp.Template.Application.Data.Exceptions;

public class DbEntityCreationException(string entityName, string paranName)
    : Exception($"Entity {entityName} could not be created. Parameter {paranName} is invalid.");
