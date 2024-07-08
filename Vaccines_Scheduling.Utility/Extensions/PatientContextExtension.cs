using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vaccines_Scheduling.Utility.PatientContext;

namespace Vaccines_Scheduling.Utility.Extensions
{
    public static class PatientContextExtension
    {
        public static string Login(this IPatientContext patientContext)
        {
            var login = patientContext.GetClaimValue<string>("login");

            return login ?? Environment.MachineName;
        }

        public static int Id(this IPatientContext patientContext)
        {
            int.TryParse(patientContext.GetClaimValue<string>(ClaimTypes.Sid), out var id);

            return id;
        }

        public static void AddData<TValue>(this IPatientContext patientContext, string key, TValue data)
        {
            patientContext.AdditionalData ??= new Hashtable();

            if (!patientContext.AdditionalData.ContainsKey(key))
                patientContext.AdditionalData.Add(key, data);
            else
                patientContext.AdditionalData[key] = data;
        }

        private static TResult? GetClaimValue<TResult>(this IPatientContext patientContext, string key)
        {
            if (patientContext?.AdditionalData is Hashtable additionalData && additionalData.ContainsKey(key))
                try { return (TResult)additionalData[key]; } catch { return default; }

            return default;
        }
    }
}
