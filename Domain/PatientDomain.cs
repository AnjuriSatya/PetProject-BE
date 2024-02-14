using BEPetProjectDemo.Common.Model;
using BEPetProjectDemo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BEPetProjectDemo.Domain
{
    public class PatientDomain:IPatientDomain
    {
        private readonly IPatientDAL _patientDAL;
        public PatientDomain(IPatientDAL patientDAL)
        {
            _patientDAL = patientDAL;
        }
        public async Task<IActionResult> GetallPatients(HttpRequestMessage req, IEnumerable<PatientsInfo> patient)
        {
         return await _patientDAL.GetallPatients(req, patient);
        }
        public async Task<IActionResult> GetPatientsById(HttpRequestMessage req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
        {
            return await _patientDAL.GetPatientsById(req, id, documentContainer);
        }

        public async Task<IActionResult> CreatePatient(PatientsInfo data, Microsoft.Azure.Cosmos.Container documentContainer)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(data, new ValidationContext(data), validationResults, true))
            {
                string invalidDataMessage = validationResults.Select(v => v.ErrorMessage).FirstOrDefault();
                return PatientLogic.CreateBadResponse(invalidDataMessage);
            }
            return await _patientDAL.CreatePatient(data, documentContainer);
        }
        public async Task<IActionResult> UpdatePatient(PatientsInfo data, string id, Microsoft.Azure.Cosmos.Container documentContainer)
        {
            return await _patientDAL.UpdatePatient(data, id, documentContainer);
        }
        public async Task<IActionResult> DeletePatient(HttpRequestMessage req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
        {
            return await _patientDAL.DeletePatient(req, id, documentContainer);
        }
    }
}

