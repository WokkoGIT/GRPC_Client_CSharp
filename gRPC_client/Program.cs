
using System.Security.Cryptography.X509Certificates;
using Com.Example.Grpc;
using Grpc.Net.Client;

namespace GrpcClient
{
    class Program
    {

        static async Task Main(string[] args)
        {
            string currDir = Environment.CurrentDirectory.ToString()+ "\\ca-cert.pem";
            var cert = new X509Certificate2(currDir);
           

            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(cert);

            //Пока не разобрался как доверие сертификату предоставить
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var httpClient = new HttpClient(handler);

            var channel = GrpcChannel.ForAddress("https://localhost:465/", new GrpcChannelOptions
            {
                HttpClient = httpClient,
                ThrowOperationCanceledOnCancellation = true
            }) ;

            var client = new GreetingService.GreetingServiceClient(channel);
            var response = await client.greetingAsync(new HelloRequest { Txid = 5, RefundAddress = "String", Name = ".net Client", OrderID = 3563456, UserID = 7356345 });
            System.Console.WriteLine(response.Greeting);
            

        }

    }
    }

