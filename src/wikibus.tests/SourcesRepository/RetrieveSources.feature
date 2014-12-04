   Feature: Retrieve sources from repository
   Verify that models are correctly deserialized from RDF

Scenario: Get simple brochure
   Given In-memory query processor
   And RDF data:
      """
      @base <http://wikibus.org/> .
      @prefix dcterms: <http://purl.org/dc/terms/>.

      {
         <brochure/VanHool+T8> a <ontology#Brochure> ;
            dcterms:title "VanHool T8 - New Look" .
      }
      """
   When brochure <http://wikibus.org/brochure/VanHool+T8> is fetched
   Then the brochure should have title 'VanHool T8 - New Look'
   
Scenario: Get brochure with Polish diacritics
   Given In-memory query processor
   And RDF data:
      """
      @base <http://wikibus.org/> .
      @prefix dcterms: <http://purl.org/dc/terms/>.

      {
         <brochure/12345> a <ontology#Brochure> ;
            dcterms:title "Jelcz M11 - nowość" .
      }
      """
   When brochure <http://wikibus.org/brochure/12345> is fetched
   Then the brochure should have title 'Jelcz M11 - nowość'
