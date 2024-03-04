
using System.Data;
using Microsoft.AspNetCore.Mvc;

public class PersonService
{
    private DataClient _connection;
    public PersonService(DataClient connection){
        _connection = connection;
    }
    public List<PersonModel>? GetAll(){
        try{
            var result = _connection.GetResultsFromQuery<PersonModel>("SELECT * FROM [HumanResources].[vEmployee]", Map);           
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<PersonModel>? GetPersonByName(string name){
        try{
            var result = _connection.GetResultsFromQuery<PersonModel>(
                "SELECT * " +
                "FROM [AdventureWorks2019].[HumanResources].[vEmployee] " +
                $"WHERE CONCAT(FirstName,' ',MiddleName,' ',LastName) LIKE '%{name}%'", Map);
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<PersonModel>? GetPersonByPersonType(string personType){
        try{
            var result = _connection.GetResultsFromQuery<PersonModel>(
                "SELECT A.* " +
                "FROM [AdventureWorks2019].[HumanResources].[vEmployee] A " +
                "INNER JOIN Person.Person B ON a.BusinessEntityID = b.BusinessEntityID " +
                $"WHERE B.PersonType = '{personType}'", Map);
            return result;
        }catch (Exception ex){
            Console.WriteLine($"Error message: {ex.Message}");
        }
        return null;
    }

    public List<PersonModel>? GetPersonByNameAndPersonType(string name, string personType){
        try{
            var result = _connection.GetResultsFromQuery<PersonModel>(
                "SELECT * " +
                $"FROM [AdventureWorks2019].[HumanResources].[vEmployee] A " +
                $"INNER JOIN Person.Person B ON A.BusinessEntityID = B.BusinessEntityID " +
                $"WHERE " +
                $"    ('{name}' ='' OR '{name}' IS NULL OR CONCAT(A.FirstName, ' ', A.MiddleName, ' ', A.LastName) LIKE '%{name}%') " +
                $"    AND " +
                $"    ('{personType}' = '' OR '{personType}' IS NULL OR B.PersonType = '{personType}')", Map);
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }
    public PersonModel Map(IDataRecord record){
        PersonModel person = new PersonModel();
            person.BusinessEntityID = (int)record["BusinessEntityID"];
            person.Title = record["Title"] as string;
            person.FirstName = record["FirstName"] as string;
            person.MiddleName = record["MiddleName"] as string;
            person.LastName = record["LastName"] as string;
            person.Suffix = record["Suffix"] as string;
            person.JobTitle = record["JobTitle"] as string;
            person.PhoneNumber = record["PhoneNumber"] as string;
            person.PhoneNumberType = record["PhoneNumberType"] as string;
            person.EmailAddress = record["EmailAddress"] as string;
            person.EmailPromotion = (int)record["EmailPromotion"];
            person.AddressLine1 = record["AddressLine1"] as string;
            person.AddressLine1 = record["AddressLine1"] as string;
            person.City = record["City"] as string;
            person.StateProvinceName = record["StateProvinceName"] as string;
            person.PostalCode = record["PostalCode"] as string;
            person.CountryRegionName = record["CountryRegionName"] as string;
            person.AdditionalContactInfo = record["AdditionalContactInfo"] as string;
            return person;
    }
}
