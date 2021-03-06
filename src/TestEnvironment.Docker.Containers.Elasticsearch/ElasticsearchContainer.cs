﻿using Docker.DotNet;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace TestEnvironment.Docker.Containers.Elasticsearch
{
    public class ElasticsearchContainer : Container
    {
        public ElasticsearchContainer(DockerClient dockerClient, string name, string imageName = "docker.elastic.co/elasticsearch/elasticsearch-oss", string tag = "6.2.4", IDictionary<string, string> environmentVariables = null, bool isDockerInDocker = false, bool reuseContainer = false, ILogger logger = null)
            : base(dockerClient, name, imageName, tag,
                new Dictionary<string, string> { ["discovery.type"] = "single-node" }.MergeDictionaries(environmentVariables),
                isDockerInDocker, reuseContainer, new ElasticsearchContainerWaiter(logger), new ElasticsearchContainerCleaner(), logger)
        {
        }

        public string GetUrl() => IsDockerInDocker ? $"http://{IPAddress}:9200" : $"http://localhost:{Ports[9200]}";
    }
}
