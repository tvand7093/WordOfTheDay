Feature: Opens url for word of the day
  When a user clicks on the word of the day
  a web browser will load that words info.

Scenario: Can open URL
  Given I am on the HomeScreen
  Then tapping the word opens the url
