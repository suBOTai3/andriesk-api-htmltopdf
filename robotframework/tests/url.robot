*** Settings ***
Resource  ../Resources/common_resource.robot
Resource  ../Resources/url_resource.robot
Library  JSONLibrary

*** Variables ***
${badHttpCode}      400
${codev}    033
 
*** Test Cases ***
 
Check for pdf from url
    [tags]  url 
    [Documentation]  Should get pdf from url
    ${jsonResponse}=   url_resource.Get Pdf From Url   ${url}  ${BASE_URL}  ${path}
    Should Not Be Empty  ${jsonResponse} 

Check Invalid request
    [tags]  url
    [Documentation]  Should get error 400 from incorrect request object
    ${jsonResponse}=  url_resource.Get Invalid Response From Bad Url  "http://invalidrequest.com.io.za"  ${BASE_URL}  ${path} 
    ${message}=   Get Value From Json   ${jsonResponse}  $..message    
    Log To Console   ${message}
    #Should Be Equal As Strings  ${message}  ${errorMessage} 