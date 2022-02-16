// Jenkins pipeline file 

pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                echo 'Building..'
                dotnetBuild ([project:'TechReads.sln'])
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
                dotnetTest ([project:'TechReads.sln'])
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}
