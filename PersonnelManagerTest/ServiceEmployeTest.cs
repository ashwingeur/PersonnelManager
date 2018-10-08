using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelManager.Business.Exceptions;
using PersonnelManager.Business.Services;
using PersonnelManager.Dal.Entites;

namespace PersonnelManagerTest
{
    [TestClass]
    public class ServiceEmployeTest
    {
       
        [DataTestMethod]
        [DataRow("0")]
        [DataRow("-10")]
        public void ValiderTaux(string rawPrice)
        {
            var salaireMensuel = decimal.Parse(rawPrice);
            var serviceEmploye = new ServiceEmploye(null);
            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                var cadre = new Cadre()
                {
                    Nom = "vivien",
                    Prenom ="Laura",
                    DateEmbauche = new DateTime (2018,05,10),
                    SalaireMensuel = salaireMensuel

                };
      
                serviceEmploye.EnregistrerCadre(cadre);
            });
            Assert.AreEqual("Taux horaire invalide", exception.Message);
        }
    }
}

