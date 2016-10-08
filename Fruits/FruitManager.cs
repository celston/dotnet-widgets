using System;
using System.Collections.Generic;

using Gnosis.Data;
using Gnosis.Entities.Data;
using Gnosis.Entities.Validators;

using Fruits.Bananas;
using Fruits.Oranges;

namespace Fruits
{
    public class FruitManager
    {
        #region Private Fields

        private FruitValidator fruitValidator = new FruitValidator();
        private ConfigurationConnectionBuilder connectionBuilder = new ConfigurationConnectionBuilder("WidgetsDenormalized");
        private EntityDataManager entityDataManager;
        private FruitDataManager fruitDataManager;

        #endregion

        #region Constructors

        public FruitManager()
        {
            entityDataManager = new EntityDataManager(connectionBuilder);
            fruitDataManager = new FruitDataManager(connectionBuilder);
        }

        #endregion

        #region Public Methods

        public Guid CreateValenciaOrange(IValenciaOrangeCreateRequest request)
        {
            return DoCreateFruit("ValenciaOrange", request);
        }

        public Guid UpdateValenciaOrange(IValenciaOrangeUpdateRequest request)
        {
            return DoUpdateFruit(request);
        }

        public ValenciaOrange RetrieveValenciaOrange(Guid id)
        {
            return DoRetrieveFruit<ValenciaOrange>(id, "ValenciaOrange");
        }

        public Guid CreateCavendishBanana(ICavendishBananaCreateRequest request)
        {
            return DoCreateFruit("CavendishBanana", request);
        }

        public CavendishBanana RetrieveCavendishBanana(Guid id)
        {
            return DoRetrieveFruit<CavendishBanana>(id, "CavendishBanana");
        }

        public ICollection<Fruit> RetrieveAllFruit()
        {
            return fruitDataManager.RetrieveAllFruit();
        }

        #endregion

        #region Private Methods

        private T DoRetrieveFruit<T>(Guid id, string type)
            where T : Fruit, new()
        {
            EntityRetrieveValidator entityRetrieveValidator = new EntityRetrieveValidator();
            entityRetrieveValidator.Validate(entityDataManager, id);

            T entity = new T();
            fruitDataManager.HydrateFruit(id, type, entity);

            return entity;
        }

        private Guid DoCreateFruit(string type, IFruitCreateRequest request)
        {
            EntityCreateValidator entityCreateValidator = new EntityCreateValidator();
            entityCreateValidator.Validate(entityDataManager, request);

            fruitValidator.Validate(request);

            Guid revision = Guid.NewGuid();
            fruitDataManager.CreateFruit(type, revision, request);

            return revision;
        }

        private Guid DoUpdateFruit(IFruitUpdateRequest request)
        {
            EntityUpdateValidator entityUpdateValidator = new EntityUpdateValidator();
            entityUpdateValidator.Validate(entityDataManager, request);

            fruitValidator.Validate(request);

            Guid revision = Guid.NewGuid();
            fruitDataManager.UpdateFruit(revision, request);

            return revision;
        }

        #endregion
    }
}
