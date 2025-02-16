namespace WebAPI.FunctionalTests.Abstraction;

public abstract class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{

    public BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
    }
    
    protected HttpClient HttpClient { get; init;}
}