pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0'
            args '-v //c/Users/kahyong.chua/Downloads:/mnt/Downloads'
        }
    }

    environment {
        DOTNET_CLI_HOME = '/tmp/.dotnet'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
        DEPLOY_PATH = '/mnt/Downloads' // Path inside the container mapped to the host folder
    }

    stages {
        stage('Restore') {
            steps {
                sh 'dotnet restore'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release'
            }
        }
        stage('Publish') {
            steps {
                sh 'dotnet publish --configuration Release --output ./published'
            }
        }
        stage('Print Publish Location') {
            steps {
                sh 'echo "Published files are located at: $(pwd)/published"'
                sh "echo Deploying to : ${DEPLOY_PATH}"
            }
        }
        stage('Deploy to Local Drive') {
            steps {
                sh """
                if [ ! -d "${DEPLOY_PATH}" ]; then
                    mkdir -p "${DEPLOY_PATH}"
                fi
                cp -r ./published/* "${DEPLOY_PATH}/"
                echo "Files have been copied to ${DEPLOY_PATH}"
                """
            }
        }
    }

    post {
        always {
            // Clean up the workspace
            cleanWs()
        }
    }
}
