using System.Text.Json.Serialization;

namespace Domain.DTOs;

public class CustomerDTO
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    //public AccountDTO Account { get; set; }
}
