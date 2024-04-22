using APIWeb.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{

    [HttpGet("get-currency-values-by-spreadsheet")]
    public async Task<IActionResult> GetCurrencyValuesBySpreadsheet()
    {
        try
        {
            var spreadsheetUrl = "https://docs.google.com/spreadsheets/d/1rzKZX0eI5dciHwo4qkSZt-WJihs8Bc_92j--gBd64Aw/edit?usp=sharing";
            var spreadsheetId = new Uri(spreadsheetUrl).PathAndQuery.Split('/')[2];

            var googleCredentialsJson = @"{
            'type': 'service_account',
            'project_id': 'your-project-id',
            'private_key_id': 'your-private-key-id',
            'private_key': '-----BEGIN PRIVATE KEY-----\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQC7Iem2Xq+Oq2T3\n...\n-----END PRIVATE KEY-----',
            'client_email': 'your-client-email',
            'client_id': 'your-client-id',
            'auth_uri': 'https://accounts.google.com/o/oauth2/auth',
            'token_uri': 'https://oauth2.googleapis.com/token',
            'auth_provider_x509_cert_url': 'https://www.googleapis.com/oauth2/v1/certs',
            'client_x509_cert_url': 'https://www.googleapis.com/robot/v1/metadata/x509/your-client-email'
        }";

            var credential = GoogleCredential.FromJson(googleCredentialsJson)
                .CreateScoped(SheetsService.Scope.Spreadsheets);

            var sheetsService = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Your Application Name"
            });

            var range = "Sheet1!A2:B";
            var request = sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = await request.ExecuteAsync();

            var currencyValues = new List<CurrencyValue>();
            int id = 1;

            foreach (var row in response.Values)
            {
                if (row.Count >= 2 && decimal.TryParse(row[1].ToString(), out var currencyValue))
                {
                    currencyValues.Add(new CurrencyValue
                    {
                        Id = id++,
                        Value = currencyValue
                    });
                }
            }

            return Ok(currencyValues);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}
