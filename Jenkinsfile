pipeline {
    agent any
    stages {
        stage('SonarQube') {
            steps {
                script {
                    scannerHome = tool 'SonarMSBuild'
                    scannerBuild = "${scannerHome}/SonarScanner.MSBuild.dll"
                    projectKey = env.GIT_URL.tokenize('/')[3].split("\\.")[0].replace("_", "")
                }
                withSonarQubeEnv('SonarServer') {
                    sh '''#!/bin/bash +x
                    for directory in `find $WORKSPACE -type d -name \'*Tests\'`; do
                        echo "$directory found, with parent: $(dirname $directory)"
                        dotnet add $directory package coverlet.msbuild
                        dotnet test $directory /p:CollectCoverage=true /p:CoverletOutputFormat=\"opencover\" /p:CoverletOutput=\"$WORKSPACE/coverage.opencover.xml\"
                    done'''
                    sh "dotnet ${scannerBuild} begin /k:${projectKey} /n:${projectKey} /v:1.0 /d:sonar.host.url=${SONAR_HOST_URL} /d:sonar.login=${SONAR_AUTH_TOKEN} /d:sonar.cs.opencover.reportsPaths=$WORKSPACE/coverage.opencover.xml"
                    sh 'dotnet build'
                    sh "dotnet ${scannerBuild} end /d:sonar.login=${SONAR_AUTH_TOKEN}"
                    sh "java -jar /etc/sonar-cnes-report.jar -p ${projectKey} -s ${SONAR_HOST_URL} -t ${SONAR_AUTH_TOKEN}"
                }
            }
        }
        stage("Checking Quality Gate") {
            steps {
                waitForQualityGate abortPipeline: true
            }
        }
        stage('ReportBuilder') {
            steps {
                script {
                    projectKey = env.GIT_URL.tokenize('/')[3].split("\\.")[0].replace("_", "")
                    committerEmail = sh (
                        script: 'git --no-pager show -s --format=\'%ae\'',
                        returnStdout: true
                    ).trim()
                }

                sh "dotnet /etc/sonarQube/SendSonarReports.dll ${WORKSPACE} ${committerEmail} ${projectKey}"
            }
        }
    }
}
