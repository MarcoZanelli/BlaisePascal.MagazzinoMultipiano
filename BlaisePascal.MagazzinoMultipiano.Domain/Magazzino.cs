namespace BlaisePascal.MagazzinoMultipiano.Domain
{
    public class Magazzino
    {
        public int[,] quantita;
        public string[][] RegistroCodiciArticoli;

        public Magazzino(int piani, int corridoi, int capacitaInizialePiani)
        {
            quantita = new int[piani, corridoi];

            RegistroCodiciArticoli = new string[piani][];

            for (int i = 0; i < piani; i++)
            {
                RegistroCodiciArticoli[piani] = new string[capacitaInizialePiani];
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

        public void StoccaggioArticolo(int piano, int corridoio, string codiceArticolo)
        {
            if (piano < 0 || piano >= RegistroCodiciArticoli.Length)
                throw new ArgumentException("Piano non trovato");
            
            if (corridoio < 0 || corridoio >= RegistroCodiciArticoli[piano].Length)
                throw new ArgumentException("Corridoio non trovato");

            if (string.IsNullOrEmpty(codiceArticolo))
                throw new ArgumentException("Il codice articolo non può essere vuoto");

            if (RegistroCodiciArticoli[piano][corridoio] != null)
                throw new ArgumentException("C'è già un articolo in questo corridoio");

            RegistroCodiciArticoli[piano][corridoio] = codiceArticolo;
        }



    }
}
