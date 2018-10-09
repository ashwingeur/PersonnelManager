using PersonnelManager.Business.Exceptions;
using PersonnelManager.Dal.Data;
using PersonnelManager.Dal.Entites;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelManager.Business.Services
{
    public class ServiceReleve
    {
        private readonly IDataReleve dataReleve;
        private readonly IDataEmploye dataEmploye;

        public ServiceReleve(IDataReleve dataReleve, IDataEmploye dataEmploye)
        {
            this.dataReleve = dataReleve;
            this.dataEmploye = dataEmploye;
        }

        public ServiceReleve()
        {
        }

        public IEnumerable<ReleveMensuel> GetListeRelevesMensuels(int idOuvrier)
        {
            return this.dataReleve.GetListeRelevesMensuels(idOuvrier).OrderBy(x => x.Periode.PremierJour);
        }

        public ReleveMensuel GetReleveMensuel(int idReleve)
        {
            return this.dataReleve.GetReleveMensuel(idReleve);
        }

        public void EnregistrerReleveMensuel(ReleveMensuel releveMensuel)
        {
            var ouvrier = this.dataEmploye.GetOuvrier(releveMensuel.IdOuvrier);

            if( releveMensuel.Jours.Any(x => x.NombreHeures >=13))
        
            {
                throw new BusinessException("Le nombre d'heure doit etre compris entre 1 et 13");
            }
            releveMensuel.NombreTotalHeures = releveMensuel.Jours.Sum(x => x.NombreHeures);
            releveMensuel.TauxHoraire = ouvrier.TauxHoraire;

            this.dataReleve.EnregistrerReleveMensuel(releveMensuel);
        }
    }
}
