﻿using System;
using System.Collections.Generic;

namespace Tool1_BestandSchrijven
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Lees de bestanden in van de straten 
            Dictionary<int, string> straatnaamIDStraatnaam = GegevensLezer_segmenten.LeesStraten();
            Dictionary<int, List<Segment>> straatnaamIDSegmentlijst = GegevensLezer_segmenten.LeesSegmenten();

            //We maken met deze gegevens een lijst van Straat objecten aan, hierin zitten ook nog de straten in niet-Vlaanderen
            List<Straat> straten = Stratenmaker.MaakStraten(straatnaamIDStraatnaam, straatnaamIDSegmentlijst);


            //Lees de bestanden in van gemeentes/provincies
            Dictionary<string, string> gemeenteIDProvincie = GegevensLezer_adressen.LeesProvincies();
            Dictionary<string, string> gemeentes = GegevensLezer_adressen.LeesGemeentes();
            Dictionary<string, string> stratenIDgemeentesID = GegevensLezer_adressen.LeesLink();

            //Vul een dictionary op met gecombineerde gegevens provincies, gemeentes,straten 
            Dictionary<string, Dictionary<string, List<Straat>>> provincies = DictionaryOpvuller.geefStratenDictionary(straten, gemeenteIDProvincie, gemeentes, stratenIDgemeentesID);

            int stratenteller = 0;
            foreach(KeyValuePair<string, Dictionary<string, List<Straat>>> provincie in provincies)
            {
                foreach(KeyValuePair<string, List<Straat>> gemeente in provincie.Value)
                {
                    foreach(Straat straat in gemeente.Value)
                    {
                        stratenteller++;
                    }
                }

            }
            Console.WriteLine(stratenteller);



            //Bestanden uitprinten 
            SchrijfBestand.PrintDocumenten(provincies);



        }
    }
}


//Algemene vragen: geef je je dictionaries en andere gegevens zoals straatnaamIDStraatnaam het beste door via de main? 
//Kan je met het unzippen best je path als parameter doorgeven voor de herbruikbaarheid, of beter niet? 
//Beste om alle lijsten door te geven via de main? 