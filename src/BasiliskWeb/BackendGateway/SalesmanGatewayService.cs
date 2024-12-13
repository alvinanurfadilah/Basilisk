namespace BasiliskWeb.BackendGateway;

public class SalesmanGatewayService
{
    private readonly IConfiguration _config;

    public SalesmanGatewayService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<string> Get(string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:8080");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("api/v1/salesman/all");
        var content = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        } else
        {
            content = response.StatusCode.ToString();
        }

        return content;
    }

    public async Task<string> Get(int pageNumber, int pageSize, string employeeNumber, string fullName, string level, string superiorName, string token)
    {
        token = token.Replace("\n", "").Replace("\r", "");
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:8080");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync($"api/v1/salesman?pageNumber={pageNumber}&pageSize={pageSize}&employeeNumber={employeeNumber}&fullName={fullName}&level={level}&superiorName={superiorName}");
        var content = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        } else
        {
            content = response.StatusCode.ToString();
        }

        return content;
    }

    public async Task<string> Get(string employeeNumber, string token)
    {
        token = token.Replace("\n", "").Replace("\r", "");
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:8080");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync($"api/v1/salesman/{employeeNumber}");
        var content = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        } else
        {
            content = response.StatusCode.ToString();
        }

        return content;
    }

    public async Task<string> Insert(SalesmanInsertUpdateDTO dto, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_config.GetSection("AppSettings:API").Value);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("/api/v1/salesman", dto);
        var content = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        } else
        {
            content = response.StatusCode.ToString();
        }

        return content;
    }

    public async Task<string> Update(SalesmanInsertUpdateDTO dto, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_config.GetSection("AppSettings:API").Value);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.PutAsJsonAsync("/api/v1/salesman", dto);
        var content = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        } else
        {
            content = response.StatusCode.ToString();
        }

        return content;
    }

    public async Task<string> Delete(string employeeNumber, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_config.GetSection("AppSettings:API").Value);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.DeleteAsync($"/api/v1/salesman?employeeNumber={employeeNumber}");
        var content = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        } else
        {
            content = response.StatusCode.ToString();
        }

        return content;
    }

    // SALESMAN REGION
    public async Task<string> GetRegionDropdown(string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:8080");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("api/v1/region/all");
        var content = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        } else
        {
            content = response.StatusCode.ToString();
        }

        return content;
    }

    public async Task<string> GetSalesmanRegion(string employeeNumber, string token)
    {
        token = token.Replace("\n", "").Replace("\r", "");
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:8080");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync($"api/v1/salesman/salesmanregion/{employeeNumber}");
        var content = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        } else
        {
            content = response.StatusCode.ToString();
        }

        return content;
    }

    public async Task<string> InsertSalesmanRegion(SalesmanRegionInsertDTO dto, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_config.GetSection("AppSettings:API").Value);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("/api/v1/salesman/salesmanregion", dto);
        var content = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        } else
        {
            content = response.StatusCode.ToString();
        }

        return content;
    }

    public async Task<string> DeleteSalesmanRegion(string employeeNumber, long regionId, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_config.GetSection("AppSettings:API").Value);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.DeleteAsync($"/api/v1/salesman/salesmanregion?employeeNumber={employeeNumber}&regionId={regionId}");
        var content = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        } else
        {
            content = response.StatusCode.ToString();
        }

        return content;
    }
}
