using Gnosis.Data;
using Gnosis.Entities.Data;
using Gnosis.Entities.Validators;
using People.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class PeopleManager
    {
        #region Private Fields

        private ConfigurationConnectionBuilder connectionBuilder = new ConfigurationConnectionBuilder("WidgetsDenormalized");
        private EntityDataManager entityDataManager;
        private PeopleDataManager peopleDataManager;

        #endregion

        #region Constructors

        public PeopleManager()
        {
            entityDataManager = new EntityDataManager(connectionBuilder);
            peopleDataManager = new PeopleDataManager(connectionBuilder);
        }

        #endregion

        #region Public Methods

        public Guid CreatePerson(IPersonCreateRequest request)
        {
            ValidatePersonCreateRequest(request);

            Guid revision = Guid.NewGuid();
            peopleDataManager.CreatePerson(request, revision, "Person");

            return revision;
        }

        #endregion

        private void ValidatePersonCreateRequest(IPersonCreateRequest request)
        {
            EntityCreateValidator entityCreateValidator = new EntityCreateValidator();
            entityCreateValidator.Validate(entityDataManager, request);

            PersonValidator personValidator = new PersonValidator();
            personValidator.Validate(request);
        }
    }
}
