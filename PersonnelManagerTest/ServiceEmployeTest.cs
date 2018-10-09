using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersonnelManager.Business.Exceptions;
using PersonnelManager.Business.Services;
using PersonnelManager.Dal.Data;
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

        [DataTestMethod]
        [DataRow("13")]
        public void ValiderHoraire(string rawHoraire)
        {
            var fauxDataReleve = new Mock<IDataReleve>();
            var fauxDataEmploye = new Mock<IDataEmploye>();
            var NombreHeures = decimal.Parse(rawHoraire);
            var serviceReleve = new ServiceReleve(fauxDataReleve.Object,fauxDataEmploye.Object);
            var releveMensuel = new ReleveMensuel();
     
            var exception = Assert.ThrowsException<BusinessException>(() =>
            {
                var releveJour = new ReleveJour
                {

                    Jour = new DateTime(2018, 05, 10),
                    NombreHeures = 20
                };
                releveMensuel.Jours.Add(releveJour);
                serviceReleve.EnregistrerReleveMensuel(releveMensuel);
            });
            Assert.AreEqual("Le nombre d'heure doit etre compris entre 1 et 13", exception.Message);

        }



    }
}

