*** Settings ***
Resource  common_resource.robot
Library  JSONLibrary

*** Keywords ***
Get Pdf From Url 
    [Arguments]  ${urlToConvert}  ${url}  ${path}
    ${data}=  create dictionary  url=${urlToConvert}
    ${jsonResponse}=   common_resource.Post Url   ${data}  ${url}  ${path}  200
    Log  ${jsonResponse}
    [return]  ${jsonResponse}
 
Get Invalid Response From Bad Url
    [Arguments]  ${urlToConvert}  ${url}  ${path}
    ${data}=  create dictionary  url=${urlToConvert}
    ${jsonResponse}=   common_resource.Post Url  ${data}  ${url}   ${path}  400
    Log  ${jsonResponse}
    [return]  ${jsonResponse}