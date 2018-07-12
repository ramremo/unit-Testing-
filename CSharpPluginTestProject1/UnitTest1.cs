using CSharpPluginProjectr;
using FakeXrmEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace CSharpPluginTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var context = new XrmRealContext
            {
                ProxyTypesAssembly = typeof(PluginClass1).Assembly,
                ConnectionStringName = "CRMConnectionString"
            };
            var executionContext = context.GetDefaultPluginContext();
            var target = new Entity("contact")
            {
                ["lastname"] = "Power",
                Id = Guid.NewGuid()
            };
            executionContext.MessageName = "Create";
            executionContext.Stage = 20;
            executionContext.PrimaryEntityId = target.Id;
            executionContext.PrimaryEntityName = target.LogicalName;
            executionContext.InputParameters = new ParameterCollection
            {
                new KeyValuePair<string, object>("Target", target)
            };

            context.ExecutePluginWith<PluginClass1>(executionContext);

            Assert.AreEqual("POWER1",
                ((Entity)executionContext.InputParameters["Target"]).GetAttributeValue<string>("lastname"));
        }
    }
}