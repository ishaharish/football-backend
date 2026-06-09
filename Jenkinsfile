pipeline {
    agent any

    environment {
        DOCKER_IMAGE = 'worldcuppoller-backend'
        DOCKER_TAG = "latest"
    }

    stages {
        stage('Checkout') {
            steps {
                // Checkout code from source control
                checkout scm
            }
        }
        
        stage('Build & Restore') {
            steps {
                // Restore dependencies and build the .NET project
                bat 'dotnet restore'
                bat 'dotnet build --configuration Release --no-restore'
            }
        }
        
        // Add a test stage here when unit tests are implemented
        // stage('Test') {
        //     steps {
        //         bat 'dotnet test --no-build --verbosity normal'
        //     }
        // }
        
        stage('Build Docker Image') {
            steps {
                script {
                    // Build the Docker image
                    bat "docker build -t ${DOCKER_IMAGE}:${DOCKER_TAG} ."
                }
            }
        }
    }
    
    post {
        always {
            cleanWs()
        }
        success {
            echo 'Pipeline completed successfully!'
        }
        failure {
            echo 'Pipeline failed. Please check the logs.'
        }
    }
}
