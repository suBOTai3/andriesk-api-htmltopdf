*** Settings ***
Library             RequestsLibrary
Library             String
Library             Collections
Library             JSONLibrary

Documentation  
...  https://robotframework-thailand.github.io/robotframework-jsonlibrary/JSONLibrary.html
...  https://github.com/robotframework-thailand/robotframework-jsonlibrary
...  pip3 install --upgrade robotframework-httplibrary
...  pip3 install -U robotframework-jsonlibrary
...  pip3 install -U robotframework-requests


*** Keywords ***
Post Url
    [Arguments]  ${data}  ${url}  ${path}  ${statuscode}=200
    Create Session  postsession  ${url}
    ${headers}=  Create Dictionary  Content-Type=application/json  Authorization=Bearer ${BEARER_TOKEN}  Keep-Alive=Keep-Alive: timeout=15, max=30000
    ${resp}=  Post Request  postsession  ${path}  data=${data}  headers=${headers}
    Log  ${resp.text}
    Should Be Equal As Strings  ${resp.status_code}  ${statuscode} 
    [return]  ${resp.json()}

Post Url Without Token
    [Arguments]  ${data}  ${url}  ${path}  ${statuscode}=200
    #Log  data  ${data}
    #Log  url  ${url}
    #Log  path  ${path}
    Create Session  postsession  ${url}
    ${headers}=  Create Dictionary  Content-Type=application/json   Keep-Alive=Keep-Alive: timeout=15, max=30000
    ${pdf}=  Post Request  postsession  ${path}  data=${data}  headers=${headers}
    Should Be Equal As Strings  ${pdf.status_code}  ${statuscode} 
    [return]  ${pdf.status_code}

Get Url
    [Arguments]  ${path}  ${url}
    Create Session  getsession  ${url}

    ${headers}=  Create Dictionary  Content-Type=application/json  Authorization=Bearer ${BEARER_TOKEN}  Keep-Alive=Keep-Alive: timeout=15, max=30000

    ${resp}=  Get Request  getsession  ${path}  headers=${headers}
    Log  ${resp.text}
    Should Be Equal As Strings  ${resp.status_code}  200
    [return]  ${resp.json()}
