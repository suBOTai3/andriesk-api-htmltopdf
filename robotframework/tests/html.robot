*** Settings ***
Resource  ../Resources/html_resource.robot

*** Variables ***
${errorMessage}     Could not load the specified reports drilldown data
${badHttpCode}      400
${codev}    033

  

*** Test Cases ***
Should return PDF given a URL
    [Documentation]  Getting PDF From Url
    ${jsonResponse}=   Get Pdf From Url   https://www.google.com
    ${length}=   Get Length   ${jsonResponse}
    ${code}=   Get Value From Json   ${jsonResponse}  $..code   
    Should Be Equal As Numbers  ${code[0]}  ${codev}

Should fail on incorrect request 
   [Documentation]  Getting Error from incorrect request object
    ${jsonResponse}=  Get Invalid Expansion Response   ${FtyType}  ${Period}  ${Code}   ${badHttpCode}
    ${message}=   Get Value From Json   ${jsonResponse}  $..message    
    Should Be Equal As Strings  ${message[0]}  ${errorMessage} 

Should return PDF given html
    [Tags]  html
    [Documentation]  Converts a pre-given string of html to PDF 
    ${pdfResponse}=   Get Pdf From Html   <h1>Hello world</h1>  ${url}  ${path}
    #Write Pdf to file  ${pdfResponse} # not sure how to do this - need to check file fidelity manually
    ${code}=   Get Value From Json   ${pdfResponse}  $..code   
    Should Be Equal As Numbers  200  ${pdfResponse}