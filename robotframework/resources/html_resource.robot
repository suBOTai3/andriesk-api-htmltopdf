*** Settings ***
Resource  common_resource.robot
Library  JSONLibrary
Library  OperatingSystem
Library  String

*** Keywords ***
Get Pdf From Html 
    [Arguments]  ${htmlToConvert}  ${url}  ${path}
    ${data}=  create dictionary  html=${htmlToConvert}  file=output.pdf
    ${pdfResponse}=   common_resource.Post Url Without Token  ${data}  ${url}  ${path}  200
    Log To Console  ${pdfResponse}
    [return]  ${pdfResponse}

Write Pdf to file
  [Arguments]  ${variable}
  Create File  ${EXECDIR}/output.pdf  ${variable}