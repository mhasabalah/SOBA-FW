namespace SobaFw.Client;

//we need to cheak it again
public class HttpService<TEntity> : IHttpService<TEntity>
    where TEntity : BaseEntity
{
    private readonly HttpClient _httpClient;
    private readonly IToastService _toastService;

    public HttpService(HttpClient httpClient, IToastService toastService)
    {
        _httpClient = httpClient;
        _toastService = toastService;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(string url)
    {
        IEnumerable<TEntity> viewModels = new List<TEntity>();
        try
        {
            HttpResponseMessage result = await _httpClient.GetAsync(url);
            string content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                // Can log here
                Console.WriteLine(content);
                throw new Exception(content);
            }

            Console.WriteLine("Get successfully");
            viewModels = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(content);
            return viewModels;
        }
        catch (Exception exception)
        {
            // Can log here
            Console.WriteLine(exception.Message);
            _toastService.ShowToast(ToastLevel.Error, exception.Message);
        }
        return viewModels;
    }
    public virtual async Task<TEntity> GetByIdAsync(string url)
    {
        TEntity viewModel = null;
        try
        {
            HttpResponseMessage result = await _httpClient.GetAsync(url);

            string resultContent = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                // Can log or show Toaster with message that contain that error.
                Console.WriteLine(resultContent);
                throw new Exception(resultContent);
            }

            Console.WriteLine("GetById successfully");
            viewModel = JsonConvert.DeserializeObject<TEntity>(resultContent);
            return viewModel;
        }
        catch (Exception exception)
        {
            // Can log here
            Console.WriteLine(exception.Message);
            _toastService.ShowError(exception.Message);
        }
        return viewModel;
    }
    public virtual async Task<TEntity> PostAsync(string url, TEntity viewModel)
    {
        try
        {
            var content = JsonConvert.SerializeObject(viewModel);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await _httpClient.PostAsync(url, bodyContent);

            string resultContent = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                // Can log or show Toaster with message that contain that error.
                Console.WriteLine(resultContent);
                throw new Exception(resultContent);
            }

            Console.WriteLine("Post successfully");
            viewModel = JsonConvert.DeserializeObject<TEntity>(resultContent);
            return viewModel;
        }
        catch (Exception exception)
        {
            // Can log here
            Console.WriteLine(exception.Message);
            _toastService.ShowError(exception.Message);
        }
        return viewModel;
    }
    public virtual async Task<TEntity> PutAsync(string url, TEntity viewModel)
    {
        try
        {
            var content = JsonConvert.SerializeObject(viewModel);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await _httpClient.PutAsync(url, bodyContent);

            string resultContent = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                // Can log here
                Console.WriteLine(resultContent);
                throw new Exception(resultContent);
            }

            Console.WriteLine("Put successfully");
            viewModel = JsonConvert.DeserializeObject<TEntity>(resultContent);
            return viewModel;
        }
        catch (Exception exception)
        {
            // Can log here
            Console.WriteLine(exception.Message);
            _toastService.ShowError(exception.Message);
        }
        return viewModel;
    }
    public virtual async Task<TEntity> DeleteAsync(string url)
    {
        TEntity viewModel = null;
        try
        {
            HttpResponseMessage result = await _httpClient.DeleteAsync(url);

            string resultContent = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                // Can log or show Toaster with message that contain that error.
                Console.WriteLine(resultContent);
            }

            Console.WriteLine("Delete successfully");
            viewModel = JsonConvert.DeserializeObject<TEntity>(resultContent);
            return viewModel;
        }
        catch (Exception exception)
        {
            // Can log here
            Console.WriteLine(exception.Message);
            _toastService.ShowError(exception.Message);
        }
        return viewModel;
    }
}
