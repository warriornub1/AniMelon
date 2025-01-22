pipeline {
    agent any

    stages {
        stage('Build') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/sdk:8.0'
                    reuseNode true
                }
            }
            environment {
                // Set a custom DOTNET_CLI_HOME directory
                DOTNET_CLI_HOME = "${WORKSPACE}/.dotnet"
            }
            steps {
                sh '''
                    # Ensure the DOTNET_CLI_HOME directory exists and is writable
                    mkdir -p ${DOTNET_CLI_HOME}
                    chmod -R 755 ${DOTNET_CLI_HOME}

                    # Display current directory and verify dotnet CLI
                    ls -la
                    dotnet --version

                    # Restore and build the project
                    dotnet restore
                    dotnet build --configuration Release

                    # Display the files after build
                    ls -la
                '''
            }
        }
    }
}
