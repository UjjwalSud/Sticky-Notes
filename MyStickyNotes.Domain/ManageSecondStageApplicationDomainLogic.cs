using Recruitive.DataAccess.Application_SecondStage;
using Recruitive.SearchDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recruitive.DomainLogic.Application_SecondStage
{
    public class ManageSecondStageApplicationDomainLogic:baseClass.DomainLogicBase
    {
        public int AddNewSecondStageApplication(Domain.Application.Applications applications)
        {
            ManageSecondStageApplicationDataAccess obj = new ManageSecondStageApplicationDataAccess();
            applications.FkCreatedBy = UserVariables.UserId;
            applications.FkAgencyId = UserVariables.AgencyId;
            applications.FormTypeId = Domain.Enums.Application.ApplicationFormType.SecondStageApplication;
            return obj.AddNewSecondStageApplication(applications);
        }

        public Domain.Application.Applications GetSecondStageApplicationDetailsByApplicationId(int applicationId)
        {
            ManageSecondStageApplicationDataAccess obj = new ManageSecondStageApplicationDataAccess();
            return obj.GetSecondStageApplicationDetailsByApplicationId(applicationId);
        }

        public Domain.Application.Applications CanDisplayMarkAsComplete(int applicationId)
        {
            ManageSecondStageApplicationDataAccess obj = new ManageSecondStageApplicationDataAccess();
            return obj.CanDisplayMarkAsComplete(applicationId);
        }

        public int UpdateSecondStageApplicationDetails(Domain.Application.Applications applications)
        {
            ManageSecondStageApplicationDataAccess obj = new ManageSecondStageApplicationDataAccess();
            return obj.UpdateSecondStageApplicationDetails(applications);
        }


        public ViewApplicationFormsSearchDomain GetUserSecondStageApplicationForms(ViewApplicationFormsSearchDomain viewApplicationForms)
        {
            ManageSecondStageApplicationDataAccess obj = new ManageSecondStageApplicationDataAccess();

            viewApplicationForms.PageSize = PageSize;
            viewApplicationForms.CurrentPage = CurrentPage;
            //viewApplicationForms.FkAgencyId = UserVariables.AgencyId;
            viewApplicationForms.FkCreatedBy = UserVariables.UserId;
            viewApplicationForms.FormTypeId = Domain.Enums.Application.ApplicationFormType.SecondStageApplication;
            return obj.GetUserSecondStageApplicationForms(viewApplicationForms);
        }

        public void DeleteSecondStageApplicationForm(int applicationId)
        {
            ManageSecondStageApplicationDataAccess obj = new ManageSecondStageApplicationDataAccess();
            obj.DeleteSecondStageApplicationForm(applicationId, UserVariables.UserId);
        }

        public void MarkSecondStageApplicationAsComplete(Domain.Application.Applications application)
        {
            ManageSecondStageApplicationDataAccess obj = new ManageSecondStageApplicationDataAccess();
            obj.MarkSecondStageApplicationAsComplete(application);
        }


        public ViewApplicationFormsSearchDomain GetLibrarySecondStageApplicationForms(int id, int jobid)
        {
            ManageSecondStageApplicationDataAccess obj = new ManageSecondStageApplicationDataAccess();
            int FormTypeID = Recruitive.Domain.Enums.Application.ApplicationFormType.SecondStageApplication.ToType<int>();
            return obj.GetLibrarySecondStageApplicationForms(id, jobid, FormTypeID);
        }



        public void AssignLibraryApplications(ViewApplicationFormsSearchDomain application, int jobId)
        {
            string selectedLibraryEntries = application.LstApplicationses.Where(item => item.IsSelected).Aggregate("", (current, item) => current + (item.ApplicationId + ","));
            if (selectedLibraryEntries.IndexOf(',') > 0)
            {
                selectedLibraryEntries = selectedLibraryEntries.Substring(0, selectedLibraryEntries.Length - 1);
            }
            ManageSecondStageApplicationDataAccess obj = new ManageSecondStageApplicationDataAccess();
            obj.AssignLibraryApplications(jobId, selectedLibraryEntries, UserVariables.SystemUserId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="strLibraryApplicationIds"></param>
        public void AssignLibraryApplications(int jobId, string strLibraryApplicationIds)
        {
            ManageSecondStageApplicationDataAccess obj = new ManageSecondStageApplicationDataAccess();
            obj.AssignLibraryApplications(jobId, strLibraryApplicationIds, UserVariables.SystemUserId);
        }

        public ViewApplicationFormsSearchDomain AddFromOwnCollection(short id, int jobid)
        {
            ManageSecondStageApplicationDataAccess obj = new ManageSecondStageApplicationDataAccess();
            int FK_FormTypeID = Recruitive.Domain.Enums.Application.ApplicationFormType.SecondStageApplication.ToType<int>();
            return obj.AddFromOwnCollection(jobid, UserVariables.UserId, UserVariables.SystemUserId, id, FK_FormTypeID);
        }

        public void AssignOwnCollectionApplications(ViewApplicationFormsSearchDomain application, int JobId)
        {
            string selectedEntries = application.LstApplicationses.Where(item => item.IsSelected).Aggregate("", (current, item) => current + (item.ApplicationId + ","));
            if (selectedEntries.IndexOf(',') > 0)
            {
                selectedEntries = selectedEntries.Substring(0, selectedEntries.Length - 1);
            }
            ManageSecondStageApplicationDataAccess obj = new ManageSecondStageApplicationDataAccess();
            obj.AssignOwnCollectionApplications(JobId, selectedEntries, UserVariables.UserId);

        }

        public string GetJobUrlReferenceNumberByJobGroupId(int jobgroupid, Domain.Enums.Application.ApplicationFormType applicationFormType)
        {
            string url = "";
            ManageSecondStageApplicationDataAccess obj = new ManageSecondStageApplicationDataAccess();
            url =  obj.GetJobUrlReferenceNumberByJobGroupId(jobgroupid, applicationFormType);
            return url;
        }
    }
}
