pipeline {
    agent any  // This means the pipeline can run on any available agent

    environment {
        DOTNET_CLI_HOME = '/tmp/.dotnet'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
        DEPLOY_PATH = 'C:/Users/kahyong.chua/Downloads/publish' // Path on the host machine
    }

    stages {
        stage('Restore') {
            steps {
                script {
                    echo "Restoring dependencies..."
                }
                bat 'dotnet restore'
            }
        }
        stage('Build') {
            steps {
                script {
                    echo "Building project in Release mode..."
                }
                bat 'dotnet build --configuration Release'
            }
        }
        stage('Publish') {
            steps {
                script {
                    echo "Publishing to ${DEPLOY_PATH}..."
                }
                bat """
                # Publish the project directly to the specified folder
                dotnet publish --configuration Release --output "${DEPLOY_PATH}"
                """
            }
        }
        stage('Print Publish Location') {
            steps {
                script {
                    echo "Checking published files at ${DEPLOY_PATH}..."
                }
            }
        }
    }
}
