namespace BlaisePascal.MagazzinoMultipiano.Domain
{
    public class Magazzino
    {
        private int[,] _quantita;
        private string?[][] _RegistroCodiciArticoli;

        public Magazzino(int piani, int corridoi, int capacitaPiani=4)
        {
            _quantita = new int[piani, corridoi];

            _RegistroCodiciArticoli = new string[piani][];

            for (int i = 0; i < piani; i++)
            {
                if (capacitaPiani <= 0)
                    throw new ArgumentException("La capacità dei piani deve essere maggiore di zero");
                _RegistroCodiciArticoli[piani] = new string[capacitaPiani];
            }
        }

        public void AggiungiScatole(int piano, int corridoio, int numeroScatole)
        {
            ControlloNumeroScatole(numeroScatole);

            ControlloPianoCorridoio(piano, corridoio);

            _quantita[piano, corridoio] += numeroScatole;
        }

        private void ControlloNumeroScatole(int numeroScatole)
        { 
            if (numeroScatole <= 0)
                throw new ArgumentException("Il numero di scatole deve essere maggiore di zero");
        }

        private void ControlloPianoCorridoio(int piano, int corridoio)
        {
            if (piano < 0 || piano >= _quantita.GetLength(0))
                throw new ArgumentException("Piano non trovato");

            if (corridoio < 0 || corridoio >= _quantita.GetLength(1))
                throw new ArgumentException("corridoio non trovato");

        }

        private void ControlloPianoRegistro(int piano)
        {
            if (piano < 0 || piano >= _RegistroCodiciArticoli.Length)
                throw new ArgumentException("Piano non trovato");
        }

        public void RimuoviScatole(int piano, int corridoio, int numeroScatole)
        {
            ControlloNumeroScatole(numeroScatole);

            ControlloPianoCorridoio(piano, corridoio);

            if (_quantita[piano, corridoio] <= numeroScatole)
                throw new ArgumentException("Non ci sono scatole da rimuovere");

            _quantita[piano, corridoio] -= numeroScatole;
        }

        public bool StoccaggioArticolo(int piano, int numeroDelCodice, string codiceArticolo)
        {
            ControlloPianoRegistro(piano);

            if (string.IsNullOrEmpty(codiceArticolo))
                throw new ArgumentException("Il codice articolo non può essere vuoto");

            if (numeroDelCodice < 0 || numeroDelCodice >= _RegistroCodiciArticoli[piano].Length)
                throw new ArgumentException("Numero del codice non trovato");

            if (string.IsNullOrEmpty(_RegistroCodiciArticoli[piano][numeroDelCodice]))
            {
                _RegistroCodiciArticoli[piano][numeroDelCodice] = codiceArticolo;
                return true;
            }

            return false;
        }


        private int PosizioneArticoloNelRegistroAlPiano(int piano, string codice)
        {
            for (int i = 0; i < _RegistroCodiciArticoli[piano].Length; i++)
            {
                if ( codice == _RegistroCodiciArticoli[piano][i])
                {
                    return i;
                }
            }

            throw new Exception("codice non trovato");
        }


        public void RimuoviArticolo(int piano, string codiceArticolo)
        {

            ControlloPianoRegistro(piano);

            int posizioneCodice = PosizioneArticoloNelRegistroAlPiano(piano, codiceArticolo);
           
            //shift a sinistra dei codici
            for (int i = posizioneCodice; i < _RegistroCodiciArticoli[piano].Length - 1; i++)
            {
                _RegistroCodiciArticoli[piano][i] = _RegistroCodiciArticoli[piano][i + 1];
            }

            _RegistroCodiciArticoli[piano][_RegistroCodiciArticoli[piano].Length - 1] = null;
        }

        public int PianoPiuAffollato()
        {
            int maxScatole = 0;
            int pianoPiuAffollato = -1;
            for (int piano = 0; piano < _quantita.GetLength(0); piano++)
            {
                int totaleScatole = 0;
                for (int corridoio = 0; corridoio < _quantita.GetLength(1); corridoio++)
                {
                    totaleScatole += _quantita[piano, corridoio];
                }

                if (totaleScatole > maxScatole)
                {
                    maxScatole = totaleScatole;
                    pianoPiuAffollato = piano;
                }
            }

            if (pianoPiuAffollato == -1)
                throw new ArgumentException("Non ci sono scatole nel magazzino");

            return pianoPiuAffollato;
        }

        public string CercaArticolo(string codiceArticolo)
        {
            for (int piano = 0; piano < _RegistroCodiciArticoli.Length; piano++)
            {
                for (int numeroDelCodice = 0; numeroDelCodice < _RegistroCodiciArticoli[piano].Length; numeroDelCodice++)
                {
                    if (_RegistroCodiciArticoli[piano][numeroDelCodice] == codiceArticolo)
                    {
                        return _RegistroCodiciArticoli[piano][numeroDelCodice];
                    }
                }
            }
            throw new ArgumentException("Articolo non trovato");
        }
    }
}
