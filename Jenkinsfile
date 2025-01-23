pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0'
            args '-v /c/Users/kahyong.chua/Downloads:/mnt/host_downloads' // Mount Downloads folder
        }
    }

    environment {
        DOTNET_CLI_HOME = '/tmp/.dotnet'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
        DEPLOY_PATH = '/mnt/host_downloads' // Path inside the container mapped to Downloads
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
            }
        }
        stage('Deploy to Downloads Folder') {
            steps {
                sh """
                echo "Deploying files to ${DEPLOY_PATH}..."
                if [ ! -d "${DEPLOY_PATH}" ]; then
                    echo "Creating deployment directory at ${DEPLOY_PATH}."
                    mkdir -p "${DEPLOY_PATH}"
                fi
                cp -r ./published/* "${DEPLOY_PATH}/"
                echo "Files have been successfully copied to ${DEPLOY_PATH}."
                """
            }
        }
    }
    post {
        always {
            cleanWs() // Clean up workspace after execution
        }
    }
}
