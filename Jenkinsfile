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
                    sh "dotnet ${scannerBuild} begin /k:${projectKey} /n:${projectKey} /v:1.0 /d:sonar.host.url=${SONAR_HOST_URL} /d:sonar.issuesReport.html.enable=true"
                    sh 'dotnet build'
                    sh "dotnet ${scannerBuild} end"
                    sh "java -jar /etc/sonar-cnes-report.jar -p ${projectKey} -s ${SONAR_HOST_URL}"
                }
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
