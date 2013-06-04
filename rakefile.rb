begin
  require 'bundler/setup'
  require 'fuburake'
rescue LoadError
  puts 'Bundler and all the gems need to be installed prior to running this rake script. Installing...'
  system("gem install bundler --source http://rubygems.org")
  sh 'bundle install'
  system("bundle exec rake", *ARGV)
  exit 0
end


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
    
    sln.ci_steps = ['run_phantom']
end

desc "Runs the ST suite using Phantom"
task :run_phantom => [:compile] do
    serenity "storyteller src/FubuMVC.Ajax.StoryTeller/Ajax.xml results/StoryTeller.html -b Phantom"
    artifacts = File.expand_path('artifacts', File.dirname(__FILE__))
end

def self.serenity(args)
  serenity = Platform.runtime(Nuget.tool("Serenity", "SerenityRunner.exe"), "v4.0.30319")
  sh "#{serenity} #{args}"
end