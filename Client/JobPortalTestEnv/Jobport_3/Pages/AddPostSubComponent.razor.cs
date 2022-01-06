using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using XebecPortal.Shared;

namespace XebecPortal.Client.JobPortalTestEnv.Jobport_3.Pages
{
    public partial class AddPostSubComponent
    {
        private List<JobPlatform> jobPlatform { get; set; } = new List<JobPlatform>();
        private List<JobType> jobTypes { get; set; } = new List<JobType>();

        public List<Department> Departments { get; set; } = new List<Department>();

        int[] jobPlatformHelper = new int[0];
        string[] jobPlatformHelperText = new string[0];
        int[] jobTypeHelper = new int[0];
        string[] jobTypeHelperText = new string[0];

        private static Action<string> jobTypeAction;
        private static Action<string> jobPlatformAction;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                jobTypes = await httpClient.GetFromJsonAsync<List<JobType>>("api/JobType");
                jobPlatform = await httpClient.GetFromJsonAsync<List<JobPlatform>>("api/JobPlatform");
                Departments = await httpClient.GetFromJsonAsync<List<Department>>("api/Department");
            }
            catch (Exception ex)
            {
                jobTypes = new List<JobType>();
                jobPlatform = new List<JobPlatform>();
                Departments = new List<Department>();
            }

            job.CreationDate = DateTime.Now;
            jobTypeAction = jobTypeModelData;
            jobPlatformAction = jobPlatformModelData;
            await base.OnInitializedAsync();
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            await JsRuntime.InvokeVoidAsync("jobElement");
            base.OnAfterRender(firstRender);
        }

        private void toWorkHistory()
        {
            NavigationManager.NavigateTo("/work-history");
        }

        private void toIndex()
        {
            NavigationManager.NavigateTo("/");
        }

        //This is the model that we bind to the form
        public Job job { get; set; } = new Job();

        //This is the event callback we use in the select drop down elements
        [Parameter]
        public EventCallback<int> ValueChanged
        {
            get;
            set;
        }

        private Task OnDepartmentChanged(ChangeEventArgs e)
        {
            job.DepartmentId = int.Parse(e.Value.ToString());
            return ValueChanged.InvokeAsync(job.DepartmentId);
        }

        private void jobTypeModelData(string value)
        {
            string[] fullMixedValue = value.Split(',');
            int arrayLength = fullMixedValue.Length;
            Array.Resize(ref jobTypeHelper, arrayLength / 2);
            Array.Resize(ref jobTypeHelperText, arrayLength / 2);

            for (int i = 0; i < arrayLength / 2; i++)
                jobTypeHelper[i] = int.Parse(fullMixedValue[i]);

            Array.Reverse(fullMixedValue);

            for (int i = 0; i < arrayLength / 2; i++)
                jobTypeHelperText[i] = fullMixedValue[i];
        }

        private void jobPlatformModelData(string value)
        {
            string[] fullMixedValue = value.Split(',');
            int arrayLength = fullMixedValue.Length;
            Array.Resize(ref jobPlatformHelper, arrayLength / 2);
            Array.Resize(ref jobPlatformHelperText, arrayLength / 2);

            for (int i = 0; i < arrayLength / 2; i++)
                jobPlatformHelper[i] = int.Parse(fullMixedValue[i]);

            Array.Reverse(fullMixedValue);

            for (int i = 0; i < arrayLength / 2; i++)
                jobPlatformHelperText[i] = fullMixedValue[i];
        }

        [JSInvokable]
        public static void jobTypeModel(string value)
        {
            jobTypeAction.Invoke(value);
        }

        [JSInvokable]
        public static void jobPlatformModel(string value)
        {
            jobPlatformAction.Invoke(value);
        }

        private async Task SendData()
        {
            await httpClient.PostAsJsonAsync("api/Job", job);
            await httpClient.PostAsJsonAsync("api/JobPlatform", jobPlatformHelper);
            if (jobTypeHelper != null)
                await httpClient.PostAsJsonAsync("api/JobTypeHelper", jobTypeHelper);

            if (Array.IndexOf(jobPlatformHelperText, "Facebook") != -1)
                await httpClient.PostAsJsonAsync("https://prod-170.westeurope.logic.azure.com:443/workflows/49a8ebc95f394b97a948dd59dfdcef24/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=Fm6VYnJ8pqgyoJD8v26j8ZICGlR6p82_pjgQbPGoQT4", job);

            if (Array.IndexOf(jobPlatformHelperText, "LinkedIn") != -1)
                await httpClient.PostAsJsonAsync("https://prod-43.westeurope.logic.azure.com:443/workflows/8cbad277b9d84caf9121b54eb2652dba/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=o6pIiibRSUm7H6RS_BQgElgY9PWnzjqSTqkCMULIn_0", job);

            //if (Array.IndexOf(jobPlatformHelperText, "Twitter") != -1)

            await JsRuntime.InvokeVoidAsync("alert", "Successfully Posted A New Job Post");
        }
    }
}