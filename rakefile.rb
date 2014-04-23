require 'fuburake'


FubuRake::Solution.new do |sln|
	sln.compile = {
		:solutionfile => 'src/FubuMVC.Ajax.sln'
	}
				 
	sln.assembly_info = {
		:product_name => "FubuMVC.Ajax",
		:copyright => 'Copyright 2012-2013 Josh Arnold, et al. All rights reserved.'
	}
	
	sln.ripple_enabled = true
	sln.fubudocs_enabled = true
    
    sln.assembly_bottle 'FubuMVC.Ajax'
    
    #sln.ci_steps = ['run_phantom']
	
	sln.options[:nuget_publish_folder] = 'nupkgs'
	sln.options[:nuget_publish_url] = 'https://www.myget.org/F/fubumvc-edge/'
end

#add_dependency 'ripple:publish', 'run_phantom'

desc "Runs the ST suite using Phantom"
task :run_phantom => [:compile] do
    serenity "storyteller src/FubuMVC.Ajax.StoryTeller/Ajax.xml results/StoryTeller.html -b Phantom"
    artifacts = File.expand_path('artifacts', File.dirname(__FILE__))
end

def self.serenity(args)
  serenity = Platform.runtime(Nuget.tool("Serenity", "SerenityRunner.exe"), "v4.0.30319")
  sh "#{serenity} #{args}"
end