pipeline {
    agent any

    environment {
        DOTNET_CLI_HOME = '/tmp/.dotnet'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
        DEPLOY_PATH = "C:\\inetpub\\wwwroot\\restapi"
        SITE_NAME = "test" // Replace with your IIS site name
    }

    stages {
        stage('Stop IIS Site') {
            steps {
                script {
                    echo "Stopping IIS site..."
                    bat 'powershell Stop-WebSite -Name "%SITE_NAME%"' // Stop the specific IIS site
                }
            }
        }

        // Other stages (Restore, Build, Publish) remain unchanged

        stage('Publish') {
            steps {
                script {
                    echo "Publishing project to ${DEPLOY_PATH}..."
                }
                bat """
                dotnet publish --configuration Release \
                --framework net8.0 \
                --output "${DEPLOY_PATH}" \
                /p:PublishSingleFile=false \
                /p:SelfContained=false
                """
            }
        }

        stage('Start IIS Site') {
            steps {
                script {
                    echo "Starting IIS site..."
                    bat 'powershell Start-WebSite -Name "%SITE_NAME%"' // Start the specific IIS site
                }
            }
        }
    }

    post {
        always {
            script {
                echo "Cleaning up workspace..."
            }
            cleanWs() // Cleanup
        }
    }
}
