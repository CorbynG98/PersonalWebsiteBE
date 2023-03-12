git config user.email "corbyn.greenwood.98@gmail.com"
git config user.name "Corbyn Greenwood"
# Run the main stuff
git checkout --orphan gh-pages
git --work-tree "./Tests/PersonalWebsiteBE.IntegrationTests/Actions/pages" add .
git --work-tree "./Tests/PersonalWebsiteBE.IntegrationTests/Actions/pages" commit -m "gh-pages"
echo "Pushing to gh-pages."
git push origin HEAD:gh-pages --force
git checkout -f dev
git branch -D gh-pages
echo "Push completed. Check the repo or the live page."