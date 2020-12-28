*** Settings ***
Resource  common_resource.robot
Library  JSONLibrary

*** Keywords ***
Create Database
    ${data}=  create dictionary  
    Log  Posting to url now
    ${jsonResponse}=   common_resource.Post Url   ${data}  ${url} /api/users/createdb  200
    Log  ${jsonResponse}
    [return]  ${jsonResponse}
 