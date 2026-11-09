namespace BlaisePascal.MagazzinoMultipiano.Domain
{
    public class Magazzino
    {
        public int[,] quantita;
        public string?[][] RegistroCodiciArticoli;

        public Magazzino(int piani, int corridoi, int capacitaPiani)
        {
            quantita = new int[piani, corridoi];

            RegistroCodiciArticoli = new string[piani][];

            for (int i = 0; i < piani; i++)
            {
                if (capacitaPiani <= 0)
                    throw new ArgumentException("La capacità dei piani deve essere maggiore di zero");
                RegistroCodiciArticoli[piani] = new string[capacitaPiani];
            }
        }

        public void AggiungiScatole(int piano, int corridoio, int numeroScatole)
        {
            ControlloNumeroScatole(numeroScatole);

            quantita[piano, corridoio] += numeroScatole;
        }

        public void ControlloNumeroScatole(int numeroScatole)
        { 
            if (numeroScatole <= 0)
                throw new ArgumentException("Il numero di scatole deve essere maggiore di zero");
        }

        public void RimuoviScatole(int piano, int corridoio, int numeroScatole)
        {
            ControlloNumeroScatole(numeroScatole);
            if (quantita[piano, corridoio] < numeroScatole)
                throw new ArgumentException("Non ci sono scatole da rimuovere");
            quantita[piano, corridoio] -= numeroScatole;
        }

        public void StoccaggioArticolo(int piano, int numeroDelCodice, string codiceArticolo)
        {
            if (piano < 0 || piano >= RegistroCodiciArticoli.Length)
                throw new ArgumentException("Piano non trovato");
            
            if (numeroDelCodice < 0 || numeroDelCodice >= RegistroCodiciArticoli[piano].Length)
                throw new ArgumentException("Numero del codice non trovato");

            if (string.IsNullOrEmpty(codiceArticolo))
                throw new ArgumentException("Il codice articolo non può essere vuoto");

            RegistroCodiciArticoli[piano][numeroDelCodice] = codiceArticolo;
        }

        public void RimuoviArticolo(int piano, int numeroDelCodice)
        {

            if (piano < 0 || piano >= RegistroCodiciArticoli.Length)
                throw new ArgumentException("Piano non trovato");

            if (numeroDelCodice < 0 || numeroDelCodice >= RegistroCodiciArticoli[piano].Length)
                throw new ArgumentException("Numero del codice non trovato");

            if (RegistroCodiciArticoli[piano][numeroDelCodice] == null)
                throw new ArgumentException("Nessun articolo presente alla posizione specificata");

            for (int i = numeroDelCodice; i < RegistroCodiciArticoli[piano].Length - 1; i++)
            {
                RegistroCodiciArticoli[piano][i] = RegistroCodiciArticoli[piano][i + 1];
            }

            RegistroCodiciArticoli[piano][RegistroCodiciArticoli[piano].Length - 1] = null;
        }

        public void PianoPiuAffollato()
        {
            int maxScatole = 0;
            int pianoPiuAffollato = -1;
            for (int piano = 0; piano < quantita.GetLength(0); piano++)
            {
                int totaleScatole = 0;
                for (int corridoio = 0; corridoio < quantita.GetLength(1); corridoio++)
                {
                    totaleScatole += quantita[piano, corridoio];
                }

                if (totaleScatole > maxScatole)
                {
                    maxScatole = totaleScatole;
                    pianoPiuAffollato = piano;
                }
            }

            if (pianoPiuAffollato == -1)
                throw new ArgumentException("Non ci sono scatole nel magazzino");
        }

        public string CercaArticolo(string codiceArticolo)
        {
            for (int piano = 0; piano < RegistroCodiciArticoli.Length; piano++)
            {
                for (int numeroDelCodice = 0; numeroDelCodice < RegistroCodiciArticoli[piano].Length; numeroDelCodice++)
                {
                    if (RegistroCodiciArticoli[piano][numeroDelCodice] == codiceArticolo)
                    {
                        return RegistroCodiciArticoli[piano][numeroDelCodice];
                    }
                }
            }
            throw new ArgumentException("Articolo non trovato");
        }
    }
}
