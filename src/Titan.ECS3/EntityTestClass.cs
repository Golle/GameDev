using System.Collections.Generic;
using Titan.ECS3.Entities;

namespace Titan.ECS3
{


    public class EntityTestClass
    {
        public void Run()
        {
            var manager = new EntityManager(10_000);

            var entity = manager.Create();
            var entity1 = manager.Create();
            var entity2 = manager.Create();
            var entity3 = manager.Create();
            var entity4 = manager.Create();

            manager.Attach(entity.Id, entity1.Id);
            manager.Attach(entity1.Id, entity2.Id);
            manager.Attach(entity1.Id, entity3.Id);
            manager.Attach(entity2.Id, entity4.Id);
            

            manager.Detach(entity3.Id);

            manager.Destroy(entity.Id);
            var info = new EntityInfo();

            info.Children = new List<uint>();

            info = default;



        }
    }
}
